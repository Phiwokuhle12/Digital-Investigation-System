function InvestigationForm({
    suspects,
    selectedSuspect,
    conclusion,
    onSuspectChange,
    onConclusionChange,
    onSubmit
}) {
    return (
        <div className="investigation-form">

            <div className="form-group">
                <label htmlFor="suspect">
                    Suspect
                </label>

                <select
                    id="suspect"
                    className="suspect-select"
                    value={selectedSuspect}
                    onChange={(e) => onSuspectChange(e.target.value)}
                >
                    <option value="">
                        Select Suspect
                    </option>

                    {suspects.map((suspect) => (
                        <option
                            key={suspect.suspectID}
                            value={suspect.suspectID}
                        >
                            {suspect.suspectName}
                        </option>
                    ))}
                </select>
            </div>

            <div className="form-group">
                <label htmlFor="conclusion">
                    Conclusion
                </label>

                <textarea
                    id="conclusion"
                    value={conclusion}
                    onChange={(e) =>
                        onConclusionChange(e.target.value)
                    }
                    placeholder="Enter your investigation conclusion..."
                    rows="7"
                />
            </div>

            <button
                type="button"
                className="primary-button"
                onClick={onSubmit}
            >
                Submit Investigation
            </button>

        </div>
    )
}

export default InvestigationForm