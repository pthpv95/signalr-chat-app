import AuthService from './AuthService';

const BASE_URL = process.env.VUE_APP_BASE_API_URL;

const authService = new AuthService();

const getAsync = async (url) => {
  const token = await authService.getToken();
  const response = await fetch(url, {
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    }
  });
  return response.json();
}

const postAsync = async (url, payload) => {
  const token = await authService.getToken();
  const response = await fetch(url, {
    method: 'POST',
    cache: 'no-cache',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    },
    body: JSON.stringify(payload)
  })

  return response.json();
}

const uploadFile = async (file) => {
  const url = BASE_URL + 'files';
  // const token = await authService.getToken();
  const formData = new FormData();
  formData.append('file', file);

  const response = await fetch(url, {
    method: 'POST',
    body: formData
  })
  return response.json();
}

export { getAsync, postAsync, uploadFile, BASE_URL }
