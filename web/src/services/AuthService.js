/* eslint-disable no-console */
import oidc from "oidc-client"
// import jwtDecode from "jwt-decode"

const userManager = new oidc.UserManager({
  userStore: new oidc.WebStorageStateStore(),
  authority: process.env.VUE_APP_AUTHORITY,
  client_id: "spa",
  redirect_uri: process.env.VUE_APP_REDIRECT_URL,
  response_type: "id_token token",
  scope: "openid api1 profile",
  post_logout_redirect_uri: window.location.origin + "/index.html",
  silent_redirect_uri: window.location.origin + "/silent-renew",
  accessTokenExpiringNotificationTime: 10,
  automaticSilentRenew: true,
  filterProtocolClaims: true,
  loadUserInfo: true,
})

userManager.events.addAccessTokenExpiring(() => {
  console.log("token expiring");
})

userManager.events.addAccessTokenExpired(() => {
  console.log('token expired');
  userManager.signinRedirect();
  localStorage.removeItem('access_token');
})

// oidc.Log.logger = console
// oidc.Log.Level = oidc.Log.ERROR

export default class AuthService {
  renewTokenManually() {
    return new Promise((resolve, reject) => {
      userManager
        .signinSilent()
        .then(user => {
          if (user) {
            return resolve(user)
          } else {
            this.signIn(null)
            return resolve(null)
          }
        })
        .catch(error => {
          return reject(error)
        })
    })
  }

  getToken() {
    return new Promise((resolve, reject) => {
      userManager
        .getUser()
        .then(user => {
          if (user == null) {
            this.signIn(null)
            return resolve(null)
          } else {
            return resolve(user.access_token)
          }
        })
        .catch(error => {
          return reject(error)
        })
    })
  }

  getSignIn() {
    return new Promise((resolve, reject) => {
      userManager
        .getUser()
        .then(user => {
          if (user == null) {
            this.signIn(null)
            return resolve(false)
          } else {
            localStorage.setItem("access_token", user.access_token)
            return resolve(true)
          }
        })
        .catch(error => {
          return reject(error)
        })
    })
  }

  // Get the profile of the user logged in
  getProfile() {
    return new Promise((resolve, reject) => {
      userManager
        .getUser()
        .then(user => {
          if (user == null) {
            this.signIn(null)
            return resolve(null)
          } else {
            return resolve(user.profile)
          }
        })
        .catch(function(err) {
          console.log(err)
          return reject(err)
        })
    })
  }

  // Redirect of the current window to the authorization endpoint.
  signIn() {
    userManager.signinRedirect().catch(err => {
      console.log(err)
    })
  }

  signinRedirectCallback(signinParams) {
    return new Promise((resolve, reject) => {
      userManager
        .signinRedirectCallback(signinParams)
        .then(user => {
          if (user) {
            resolve(user)
          } else {
            this.signIn()
            resolve(null)
          }
        })
        .catch(error => {
          return reject(error)
        })
    })
  }

  // Redirect of the current window to the authorization endpoint.
  signOut() {
    userManager.signoutRedirect().then(() => {}).catch(err => {
      console.log(err)
    })
  }

  signinSilent(){
    userManager.signinSilentCallback()
      .then(() => {
      console.log('renew success');
    }).catch((error) => console.log(error));
  }
}
