function SuspectCard({ suspect, onSelect }) {
    return (
        <div className="suspect-card">
            <h2>{suspect.suspectName}</h2>

            <h4>{suspect.occupation}</h4>

            <p>{suspect.description}</p>

            <button onClick={() => onSelect(suspect)}>
                Select Suspect
            </button>
        </div>
    );
}

export default SuspectCard;