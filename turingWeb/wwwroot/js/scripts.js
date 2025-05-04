document.addEventListener('DOMContentLoaded', () => {
    const stateList = document.getElementById('state-list');
    const addStateButton = document.getElementById('add-state');

    const createStateItem = (state = { name: '', isFinal: false, transitions: [] }) => {
        const li = document.createElement('li');

        // State name input
        const nameInput = document.createElement('input');
        nameInput.type = 'text';
        nameInput.value = state.name;
        nameInput.placeholder = 'State Name';

        // Save state on <Return> key press
        nameInput.addEventListener('keydown', (event) => {
            if (event.key === 'Enter') {
                if (!nameInput.value.trim()) {
                    alert('State name cannot be empty.');
                    return;
                }
                nameInput.disabled = true;
                addTransitionButton.style.display = 'inline-block';
                finalCheckbox.disabled = false;
            }
        });

        // Final state checkbox
        const finalCheckbox = document.createElement('input');
        finalCheckbox.type = 'checkbox';
        finalCheckbox.checked = state.isFinal;
        finalCheckbox.disabled = true; // Disabled until the state name is saved
        const finalLabel = document.createElement('label');
        finalLabel.textContent = 'Final';
        finalLabel.style.marginLeft = '8px';

        // Transitions container
        const transitionsContainer = document.createElement('div');
        transitionsContainer.className = 'transitions-container';

        // Add transition button (hidden initially)
        const addTransitionButton = document.createElement('button');
        addTransitionButton.textContent = 'Add Transition';
        addTransitionButton.style.display = 'none'; // Hidden until state name is saved
        addTransitionButton.addEventListener('click', () => {
            createTransitionInput(transitionsContainer);
        });

        // Remove state button (styled as "X" in a gray box)
        const removeButton = document.createElement('button');
        removeButton.textContent = 'X';
        removeButton.style.backgroundColor = 'gray';
        removeButton.style.color = 'white';
        removeButton.style.border = 'none';
        removeButton.style.borderRadius = '4px';
        removeButton.style.padding = '4px 8px';
        removeButton.style.cursor = 'pointer';
        removeButton.addEventListener('click', () => {
            stateList.removeChild(li);
        });

        li.appendChild(nameInput);
        li.appendChild(finalCheckbox);
        li.appendChild(finalLabel);
        li.appendChild(transitionsContainer);
        li.appendChild(addTransitionButton);
        li.appendChild(removeButton);

        stateList.appendChild(li);
    };

    const createTransitionInput = (container, transition = '') => {
        const transitionInputWrapper = document.createElement('div');
        transitionInputWrapper.className = 'transition-input-wrapper';

        const transitionInput = document.createElement('input');
        transitionInput.type = 'text';
        transitionInput.value = transition;
        transitionInput.placeholder = 'Transition';

        const removeTransitionButton = document.createElement('button');
        removeTransitionButton.textContent = 'X';
        removeTransitionButton.style.backgroundColor = 'gray';
        removeTransitionButton.style.color = 'white';
        removeTransitionButton.style.border = 'none';
        removeTransitionButton.style.borderRadius = '4px';
        removeTransitionButton.style.padding = '4px 8px';
        removeTransitionButton.style.cursor = 'pointer';
        removeTransitionButton.addEventListener('click', () => {
            container.removeChild(transitionInputWrapper);
        });

        transitionInputWrapper.appendChild(transitionInput);
        transitionInputWrapper.appendChild(removeTransitionButton);
        container.appendChild(transitionInputWrapper);
    };

    addStateButton.addEventListener('click', () => {
        createStateItem();
    });
});
// document.addEventListener('DOMContentLoaded', () => {
//     const turingRoot = document.getElementById('turingRoot');
//     const turingInstructions = document.getElementById('turingInstructions');

//     if (!turingRoot || !turingInstructions) {
//         console.error('Required DOM elements not found.');
//         return;
//     }

//     const createTuringMachine = async () => {
//         const instructions = turingInstructions.value;

//         if (!instructions) {
//             alert('Please provide Turing Machine instructions.');
//             return;
//         }

//         try {
//             const response = await fetch('/service/turing', {
//                 method: 'POST',
//                 headers: {
//                     'Content-Type': 'application/json',
//                 },
//                 body: JSON.stringify({ instructions }),
//             });

//             if (!response.ok) {
//                 throw new Error('Failed to create Turing Machine');
//             }

//             const data = await response.json();
//             console.log('Turing Machine created:', data);
//             alert('Turing Machine created successfully!');
//         } catch (error) {
//             console.error('Error:', error);
//             alert('Error creating Turing Machine.');
//         }
//     };

//     const createButton = document.createElement('button');
//     createButton.textContent = 'Create Turing Machine';
//     createButton.addEventListener('click', createTuringMachine);

//     turingRoot.appendChild(createButton);
// });