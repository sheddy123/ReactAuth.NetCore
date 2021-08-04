import React, { useState } from 'react';
import { Redirect } from 'react-router-dom';
const Register = () => {
  const [user, setUser] = useState({
    username: '',
    email: '',
    password: '',
  });
  const [redirect, setRedirect] = useState(false);
  const handleChange = (e) => {
    const nameProp = e.target.name;
    const value = e.target.value;
    setUser({ ...user, [nameProp]: value });
  };
  const RegistrationForm = async (e) => {
    e.preventDefault();
    const response = await fetch('/api/Auth/Register', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        username: user.username,
        email: user.email,
        password: user.password,
      }),
    });
    setRedirect(true);
  };

  if (redirect) {
    return <Redirect to='/login' />;
  }
  //useEffect(() => {}, []);
  return (
    <div>
      <main className='form-signin'>
        <form onSubmit={RegistrationForm}>
          <h1 className='h3 mb-3 fw-normal'>Sign Up</h1>
          <div className='form-floating'>
            <label htmlFor='username'>Username : </label>
            <input
              type='text'
              id='idUsername'
              className='form-control'
              name='username'
              value={user.username}
              onChange={handleChange}
            />
          </div>
          <div className='form-floating'>
            <label htmlFor='email'>Email Address : </label>
            <input
              type='email'
              id='idEmail'
              className='form-control'
              name='email'
              value={user.email}
              onChange={handleChange}
            />
          </div>
          <div className='form-floating'>
            <label htmlFor='password'>Password : </label>
            <input
              type='password'
              id='idPassword'
              className='form-control'
              name='password'
              value={user.password}
              onChange={handleChange}
            />
          </div>

          <div className='checkbox mb-3'>
            <label>
              <input type='checkbox' value='remember-me' /> Remember me
            </label>
          </div>
          <button className='w-100 btn btn-lg btn-primary' type='submit'>
            Register
          </button>
          <p className='mt-5 mb-3 text-muted'>&copy; 2017–2021</p>
        </form>
      </main>
    </div>
  );
};

export default Register;
