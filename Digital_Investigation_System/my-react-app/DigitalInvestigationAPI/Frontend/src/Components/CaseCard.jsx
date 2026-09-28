import { Link } from 'react-router-dom'

function CaseCard({ caseData }) {
    return (
        <div className="case-card">
            <div className="case-card-header">
                <div>
                    <p className="case-label">ACTIVE CASE</p>
                    <h2>{caseData.caseName}</h2>
                </div>

                <span className="status-badge">
                    {caseData.status}
                </span>
            </div>

            <div className="case-card-body">
                <p>{caseData.description}</p>
            </div>

            <div className="case-card-actions">
                <Link to="/suspects" className="secondary-button">
                    View Suspects
                </Link>

                <Link to="/evidence" className="primary-button">
                    View Evidence
                </Link>
            </div>
        </div>
    )
}

export default CaseCard