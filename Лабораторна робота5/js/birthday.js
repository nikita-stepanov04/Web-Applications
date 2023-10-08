function birthday() {
    const date = document.querySelector('#date').value.split('-').map(x => parseInt(x));
    console.log(date);

    const a = Math.floor((14 - date[1]) / 12);
    const y = date[0] - a;
    const m = date[1] + 12 * a - 2;
    const dayNum = (date[2] + y + Math.floor(y / 4) - Math.floor(y / 100) +
        Math.floor(y / 400) + Math.floor((31 * m) / 12)) % 7;

    const day = {
        0: "Sunday",
        1: "Monday",
        2: "Tuesday",
        3: "Wednesday",
        4: "Thursday",
        5: "Friday",
        6: "Saturday",
    }

    document.querySelector('#birthday').innerHTML = `You were born on ${day[dayNum]}`;
}

