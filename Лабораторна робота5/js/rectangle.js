function calculate () {
    const width = document.querySelector('#width');
    const height = document.querySelector('#height');

    const perimeter = document.querySelector('#perimeter');
    const square = document.querySelector('#square');
    const diagonal = document.querySelector('#diagonal');

    if (width.value !== '' && height.value !== '') {
        const widthFloat = parseFloat(width.value);
        const heightFloat = parseFloat(height.value);
        
        if (!isNaN(widthFloat) && !isNaN(heightFloat) && heightFloat >= 0 && widthFloat >= 0) {
            perimeter.innerHTML = ((widthFloat + heightFloat) * 2).toFixed(4);
            square.innerHTML = (widthFloat * heightFloat).toFixed(4);
            diagonal.innerHTML = Math.sqrt(widthFloat ** 2 + heightFloat ** 2).toFixed(4);
        }       
    } else {
        perimeter.innerHTML = '';
        square.innerHTML = '';
        diagonal.innerHTML = '';
    }
}

