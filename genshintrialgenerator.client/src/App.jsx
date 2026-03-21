import { Routes, Route } from 'react-router-dom'
import Home from './components/Home'
import Heroes from './components/Heroes'
import Bosses from './components/Bosses'
import Header from './components/Header'
import './css/Header.css'
import './css/App.css'

const App = () => {
  return (
    <div className="app">
      <Header />
      <main className="app-content">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/heroes" element={<Heroes />} />
          <Route path="/bosses" element={<Bosses />} />
        </Routes>
      </main>
    </div>
  )
}

export default App