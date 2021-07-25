const fetchWrapper = (url, method, body = null) => {
  const token = localStorage.getItem('token');
  return fetch(process.env.NEXT_PUBLIC_BE_HOST + `/api${url}`, {
    method: method,
    headers: {
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    },
    body: body ? body : null
  }).then((res) => res.json())
    .catch((err) => {
      throw err;
    })
}

export {
  fetchWrapper
}