const iconPath = '~/images/FootballGameIcons/';
const sliceIndex = 12;

let i = document.getElementById('i');
let i_trigger = document.getElementById('i_trigger');
let i_file = document.getElementById('i_file');
handleEventListener(i_trigger, i, i_file, sliceIndex, iconPath);

function handleEventListener(trigger, element, fileElement, index, path) {
    trigger.addEventListener('click', () => {
        let fileElement_Text = fileElement.value.slice(index);
        let fullPath = path + fileElement_Text;
        element.value = fullPath;
    })
}