import { BrowserRouter, Routes, Route, Link } from 'react-router-dom'
import Case from './pages/Case'
import Suspects from './pages/Suspects'
import Evidence from './pages/Evidence'
import Investigation from './pages/Investigation'
import './App.css'

function Home() {
    return (
        <div className="page">
            <section className="hero-section">
                <p className="eyebrow">TRACEPOINT INVESTIGATIONS</p>

                <h1>THE MISSING PROTOTYPE</h1>

                <p className="hero-text">
                    A prototype has disappeared from a secure research laboratory.
                    <br />
                    <br />
                    Your task is to investigate the evidence and identify the
                    most likely suspect.
                </p>

                <Link to="/case" className="primary-button">
                    START INVESTIGATION
                </Link>
            </section>
        </div>
    )
}

function App() {
    return (
        <BrowserRouter>
            <header className="navbar">
                <Link to="/" className="logo">
                    TRACEPOINT
                </Link>

                <nav>
                    <Link to="/">Home</Link>
                    <Link to="/case">Case</Link>
                    <Link to="/suspects">Suspects</Link>
                    <Link to="/evidence">Evidence</Link>
                    <Link to="/investigation">Investigation</Link>
                </nav>
            </header>

            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/case" element={<Case />} />
                <Route path="/suspects" element={<Suspects />} />
                <Route path="/evidence" element={<Evidence />} />
                <Route path="/investigation" element={<Investigation />} />
            </Routes>
        </BrowserRouter>
    )
}

export default App