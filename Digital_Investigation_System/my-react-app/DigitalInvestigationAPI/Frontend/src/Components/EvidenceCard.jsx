function EvidenceCard({ evidence, onExamine }) {
    return (
        <div className="evidence-card">

            <div className="evidence-card-header">
                <h2>{evidence.title}</h2>
            </div>

            <div className="evidence-card-body">

                <p>
                    <strong>Location:</strong>{' '}
                    {evidence.location}
                </p>

                <p>
                    {evidence.description}
                </p>

            </div>

            <div className="evidence-card-actions">

                <button
                    className="primary-button"
                    onClick={() => onExamine(evidence)}
                >
                    Examine Evidence
                </button>

            </div>

        </div>
    )
}

export default EvidenceCard