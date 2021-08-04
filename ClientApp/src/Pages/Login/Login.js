import React, { useState, useEffect } from 'react';
import { Redirect } from 'react-router-dom';
import './Login.css';

const Login = (props) => {
  const [loginUser, setLoginUser] = useState({
    email: '',
    password: '',
  });
  const [redirect, setRedirect] = useState(false);
  const handleSubmit = (e) => {
    const nameProp = e.target.name;
    const value = e.target.value;
    setLoginUser({ ...loginUser, [nameProp]: value });
  };
  const SubmitForm = async (e) => {
    e.preventDefault();
    const response = await fetch('/api/Auth/Login', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      credentials: 'include',
      body: JSON.stringify({
        email: loginUser.email,
        password: loginUser.password,
      }),
    });
    const content = await response.json();

    props.setName(content.username);
    setRedirect(true);
  };

  if (redirect) {
    return <Redirect to='/' />;
  }

  return (
    <div>
      <main className='form-signin'>
        <form onSubmit={SubmitForm}>
          <h1 className='h3 mb-3 fw-normal'>Please sign in</h1>

          <div className='form-floating'>
            <label htmlFor='email'>Email address</label>
            <input
              type='email'
              className='form-control'
              id='floatingInput'
              name='email'
              value={loginUser.email}
              onChange={handleSubmit}
              placeholder='name@example.com'
            />
          </div>
          <div className='form-floating'>
            <label htmlFor='password'>Password</label>
            <input
              type='password'
              className='form-control'
              id='floatingPassword'
              name='password'
              onChange={handleSubmit}
              value={loginUser.password}
              placeholder='Password'
            />
          </div>

          <div className='checkbox mb-3'>
            <label>
              <input type='checkbox' value='remember-me' /> Remember me
            </label>
          </div>
          <button className='w-100 btn btn-lg btn-primary' type='submit'>
            Sign in
          </button>
          <p className='mt-5 mb-3 text-muted'>&copy; 2021</p>
        </form>
      </main>
    </div>
  );
};

export default Login;
