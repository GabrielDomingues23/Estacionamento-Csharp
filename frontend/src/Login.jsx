import { useState } from 'react';

function Login({ onLogin, status }) {
  const [username, setUsername] = useState('admin');
  const [password, setPassword] = useState('password');

  return (
    <main className="panel">
      <h2>Login</h2>
      <form
        onSubmit={(event) => {
          event.preventDefault();
          onLogin(username, password);
        }}
      >
        <label>
          Usuário
          <input
            type="text"
            value={username}
            onChange={(event) => setUsername(event.target.value)}
          />
        </label>
        <label>
          Senha
          <input
            type="password"
            value={password}
            onChange={(event) => setPassword(event.target.value)}
          />
        </label>
        <button type="submit">Entrar</button>
      </form>
      <p className="status">{status}</p>
      <p className="info">
        Use <strong>admin</strong> / <strong>password</strong> para autenticar.
      </p>
    </main>
  );
}

export default Login;
