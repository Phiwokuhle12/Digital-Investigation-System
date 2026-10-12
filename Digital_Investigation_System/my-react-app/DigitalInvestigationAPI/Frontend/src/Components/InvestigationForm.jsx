import { useState } from 'react'

function InvestigationForm({
    suspects,
    selectedSuspect,
    conclusion,
    onSuspectChange,
    onConclusionChange,
    onSubmit
}) {
    const [isSubmitting, setIsSubmitting] = useState(false)

    const handleSubmit = async () => {
        if (isSubmitting) {
            return
        }

        setIsSubmitting(true)

        try {
            await onSubmit()
        } finally {
            setIsSubmitting(false)
        }
    }

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
                    onChange={(e) =>
                        onSuspectChange(e.target.value)
                    }
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

            {/* Digital verification notice */}


            <button
                type="button"
                className="primary-button"
                onClick={handleSubmit}
                disabled={isSubmitting}
            >
                {isSubmitting
                    ? 'Generating Docket...'
                    : 'Submit Investigation'}
            </button>

        </div>
    )
}

export default InvestigationForm