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
    setStatus('Autenticando...');
    try {
      const response = await fetch('http://localhost:5132/api/estacionamento/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password }),
      });

      if (!response.ok) {
        const error = await response.json();
        setStatus(error?.mensagem || 'Falha na autenticação');
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
        <Login onLogin={handleLogin} status={status} />
      ) : (
        <Vagas token={token} onLogout={handleLogout} status={status} setStatus={setStatus} />
      )}
    </div>
  );
}

export default App;
