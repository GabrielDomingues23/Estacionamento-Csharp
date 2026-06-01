import { useEffect, useState } from 'react';

function Login({ onLogin, onRegister, status }) {
  const [mode, setMode] = useState('login');
  const [loginUsername, setLoginUsername] = useState('');
  const [loginPassword, setLoginPassword] = useState('');
  const [registerUsername, setRegisterUsername] = useState('');
  const [registerPassword, setRegisterPassword] = useState('');
  const [loginError, setLoginError] = useState('');
  const [registerError, setRegisterError] = useState('');
  const [usernameError, setUsernameError] = useState(false);

  useEffect(() => {
    if (status === 'usuario ou senha incorretos') {
      setLoginError(status);
    } else {
      setLoginError('');
    }
  }, [status]);

  const handleLoginSubmit = async (event) => {
    event.preventDefault();
    setLoginError('');
    await onLogin(loginUsername, loginPassword);
  };

  const handleRegisterSubmit = async (event) => {
    event.preventDefault();
    setRegisterError('');
    setUsernameError(false);

    if (!registerUsername.trim() || !registerPassword.trim()) {
      setRegisterError('Nome de usuário e senha são obrigatórios');
      return;
    }

    const result = await onRegister(registerUsername, registerPassword);

    if (result.success) {
      setMode('login');
      setRegisterUsername('');
      setRegisterPassword('');
      setRegisterError('');
      setUsernameError(false);
    } else {
      if (result.field === 'username') {
        setUsernameError(true);
      }
      setRegisterError(result.message);
    }
  };

  const switchToLogin = () => {
    setMode('login');
    setRegisterError('');
    setUsernameError(false);
  };

  const switchToRegister = () => {
    setMode('register');
    setLoginError('');
  };

  return (
    <main className="panel login-panel">
      {mode === 'login' ? (
        <form className="login-form" onSubmit={handleLoginSubmit}>
          <h2>Login</h2>

          <label>
            Usuário
            <input
              type="text"
              value={loginUsername}
              onChange={(event) => setLoginUsername(event.target.value)}
            />
          </label>

          <label>
            Senha
            <input
              type="password"
              value={loginPassword}
              onChange={(event) => setLoginPassword(event.target.value)}
            />
          </label>

          <div className="register-actions">
            <button type="button" className="button-secondary" onClick={switchToRegister}>
              Criar Conta
            </button>
            <button type="submit">Entrar</button>
          </div>

          {loginError && <p className="error-message">{loginError}</p>}
          {status && status !== 'usuario ou senha incorretos' && <p className="status">{status}</p>}
        </form>
      ) : (
        <form className="login-form" onSubmit={handleRegisterSubmit}>
          <h2>Criar Conta</h2>

          <label className={usernameError ? 'invalid' : ''}>
            Nome de usuário
            <input
              type="text"
              value={registerUsername}
              onChange={(event) => {
                setRegisterUsername(event.target.value);
                if (usernameError) setUsernameError(false);
              }}
            />
          </label>

          <label>
            Senha
            <input
              type="password"
              value={registerPassword}
              onChange={(event) => setRegisterPassword(event.target.value)}
            />
          </label>

          {registerError && <p className="error-message">{registerError}</p>}

          <div className="register-actions">
            <button type="button" className="button-secondary" onClick={switchToLogin}>
              cancelar
            </button>
            <button type="submit">criar conta</button>
          </div>
        </form>
      )}
    </main>
  );
}

export default Login;
