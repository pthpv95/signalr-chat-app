const login = (username, password) => {
  return fetch(process.env.NEXT_PUBLIC_BE_HOST + '/api/auth/login', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({
      username,
      password
    })
  })
    .then((res) => {
      return res.json();
    }).catch((err) => {
      throw err;
    })
}

export {
  login
}