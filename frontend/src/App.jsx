import { useEffect, useState } from 'react';
import Login from './Login';
import Vagas from './Vagas';

function App() {
  const [token, setToken] = useState(localStorage.getItem('parking_token') || '');
  const [status, setStatus] = useState('');

  useEffect(() => {
    if (token) {
      localStorage.setItem('parking_token', token);
    } else {
      localStorage.removeItem('parking_token');
    }
  }, [token]);

  const handleLogin = async (username, password) => {
    setStatus('');
    try {
      const response = await fetch('http://localhost:5132/api/estacionamento/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password }),
      });

      if (!response.ok) {
        if (response.status === 401) {
          setStatus('usuario ou senha incorretos');
          return;
        }

        const error = await response.json();
        setStatus(error?.Mensagem || 'Falha na autenticação');
        return;
      }

      const data = await response.json();
      setToken(data.token);
      setStatus('Autenticado com sucesso.');
    } catch (err) {
      console.error(err);
      setStatus('Erro de conexão com a API.');
    }
  };

  const handleRegister = async (username, password) => {
    setStatus('');
    try {
      const response = await fetch('http://localhost:5132/api/estacionamento/register', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password }),
      });

      const data = await response.json();

      if (!response.ok) {
        if (response.status === 409) {
          setStatus('nome de usuário em uso');
          return { success: false, field: 'username', message: 'nome de usuário em uso' };
        }

        setStatus(data?.Mensagem || 'Falha ao criar conta');
        return { success: false, field: null, message: data?.Mensagem || 'Falha ao criar conta' };
      }

      setStatus(data?.Mensagem || 'Conta criada com sucesso.');
      return { success: true, field: null, message: data?.Mensagem || 'Conta criada com sucesso.' };
    } catch (err) {
      console.error(err);
      setStatus('Erro de conexão com a API.');
      return { success: false, field: null, message: 'Erro de conexão com a API.' };
    }
  };

  const handleLogout = () => {
    setToken('');
    setStatus('Desconectado.');
  };

  return (
    <div className="app-container">
      <header>
        <h1>Estacionamento React</h1>
      </header>
      {!token ? (
        <Login onLogin={handleLogin} onRegister={handleRegister} status={status} />
      ) : (
        <Vagas token={token} onLogout={handleLogout} status={status} setStatus={setStatus} />
      )}
    </div>
  );
}

export default App;
