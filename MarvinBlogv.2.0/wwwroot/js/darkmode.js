function darkmode() {

    if (document.getElementById('darkmode').style.backgroundColor == 'white') {
        document.getElementById('darkmode').style.backgroundColor = 'black';
        document.getElementById('darkmode').style.color = 'white';
        document.getElementById('btn').innerHTML('Light Mode');

    }

    else {
        document.getElementById('darkmode').style.backgroundColor = 'white';
        document.getElementById('darkmode').style.color = 'black';
        document.getElementById('btn').innerHTML('Dark Mode');
    }
}