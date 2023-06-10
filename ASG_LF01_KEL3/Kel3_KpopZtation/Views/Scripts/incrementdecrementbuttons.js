const input = document.getElementById('Contents_BOTBAddedAmount');
const incrementButton = document.getElementById('incrementButton');
const decrementButton = document.getElementById('decrementButton');

incrementButton.addEventListener('click', () => {
    input.value = parseInt(input.value) + 1;
});

decrementButton.addEventListener('click', () => {
    if (parseInt(input.value) > 1) {
        input.value = parseInt(input.value) - 1;
    }
});