function SuspectCard({ suspect, onSelect }) {
    return (
        <div className="suspect-card">

            <h2>{suspect.suspectName}</h2>

            <h4>{suspect.occupation}</h4>

            <p>{suspect.description}</p>

            <div className="suspect-card-actions">
                <button
                    className="select-button"
                    onClick={() => onSelect(suspect)}
                >
                    Select Suspect
                </button>
            </div>

        </div>
    );
}

export default SuspectCard;