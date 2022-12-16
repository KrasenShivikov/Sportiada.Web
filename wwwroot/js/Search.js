const btn = document.getElementById('btn');
const tbody = document.getElementById('tbody');
const elements = Array.from(tbody.children);

btn.addEventListener('click', () => {
    const search = document.getElementById('search');
    const tbody = document.getElementById('tbody');
    const arr = Array.from(tbody.children);

    const value = search.value;
    const results = elements.filter(t => t.children[0].textContent.toLowerCase().includes(value.toLowerCase()));

    if (value != '') {
        arr.forEach(t => {
            tbody.removeChild(t);
        })

        results.forEach(r => {
            tbody.appendChild(r);
        })
    }


})