var app = new Vue({
    el: '#content',
    data: {
        lvl: 0,
        xp: 0,
        characterName: "",
        job: "Нет",
        money: 0,
        show: 0
    },
    methods: {
        renderType(data) {
            if (data == 0){
                document.body.style.display = "none";
            }
            else if (data == 1) {
                this.show = 1;
                this.bgColor = '#111520';
                document.body.style.display = "block";
            }
        }
    }
})
window.addEventListener('message', (event) => {

    if (event.data.type == 'render') {
        app.renderType(1);
    }
    else if (event.data.type == 'unrender') app.renderType(0);
});
window.post = (url, data) => {
    var request = new XMLHttpRequest();
    request.open('POST', url, true);
    request.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
    request.send(data);
  }
