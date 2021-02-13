import Vue from "vue"
import Router from "vue-router"
import SignUp from "../modules/SignUp"
import Home from "../modules/HomePage"
import Profile from "../modules/Profile"
import SilentRenew from '../modules/SilentRenew'
import ChatConversation from "../modules/Conversation"
import AuthPage from "../modules/Auth"

Vue.use(Router)
let router = new Router({
  mode: "history",
  routes: [
    {
      path: "/",
      name: Home,
      component: Home,
      meta: {
        requiresAuth: true,
      },
    },
    {
      path: "/sign-up",
      name: "sign-up",
      component: SignUp,
    },
    {
      path: "/profile",
      name: "profile",
      component: Profile,
      meta: {
        requiresAuth: true,
      },
    },
    {
      path: "/silent-renew",
      name: "silentrenew",
      component: SilentRenew,
    },
    {
      path: "/chat",
      name: "ChatBox",
      component: ChatConversation,
      meta: {
        requiresAuth: true,
      },
    },
    {
      path: "/auth",
      name: "AuthPage",
      component: AuthPage,
    },
  ],
})

router.beforeEach((to, from, next) => {
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    if (!localStorage.getItem("access_token")) {
      next({
        path: "/auth",
        params: { nextUrl: to.fullPath },
      })
    }else{
      next()
    }
  } else {
    next()
  }
})

export default router