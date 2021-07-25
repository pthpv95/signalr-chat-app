import React, { useState } from 'react';
import useLogin from '../../hooks/useLogin';

const Login = () => {
  const [username, setUsername] = useState('hp@gmail.com')
  const [password, setPassword] = useState('123456')
  const { isLoading, mutate } = useLogin(username, password)
  console.log(isLoading);
  return (
    <div>
      <form onSubmit={async (e) => {
        e.preventDefault();
        mutate()
      }}>
        <input type="text" placeholder="User name" value={username} onChange={(e) => setUsername(e.target.value)} />
        <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} />
        <button type="submit">Login</button>
      </form>
    </div>
  );
};

export default Login;