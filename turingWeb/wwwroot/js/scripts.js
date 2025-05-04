document.addEventListener('DOMContentLoaded', () => {
    const turingRoot = document.getElementById('turingRoot');
    const turingInstructions = document.getElementById('turingInstructions');

    if (!turingRoot || !turingInstructions) {
        console.error('Required DOM elements not found.');
        return;
    }

    const createTuringMachine = async () => {
        const instructions = turingInstructions.value;

        if (!instructions) {
            alert('Please provide Turing Machine instructions.');
            return;
        }

        try {
            const response = await fetch('/service/turing', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ instructions }),
            });

            if (!response.ok) {
                throw new Error('Failed to create Turing Machine');
            }

            const data = await response.json();
            console.log('Turing Machine created:', data);
            alert('Turing Machine created successfully!');
        } catch (error) {
            console.error('Error:', error);
            alert('Error creating Turing Machine.');
        }
    };

    const createButton = document.createElement('button');
    createButton.textContent = 'Create Turing Machine';
    createButton.addEventListener('click', createTuringMachine);

    turingRoot.appendChild(createButton);
});