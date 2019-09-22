Data = new Date();
var devicesApp = new Vue({
    el: '#content',
    methods: {
      phone (event) {
        // `this` внутри методов указывает на экземпляр Vue
        if (event) this.draw = true;
        devicesApp.setTime();
      },
      setTime() {
        setInterval(function () {
          devicesApp.currentHours = Data.getHours().toLocaleString();
          devicesApp.currentMinutes = Data.getMinutes() < 10 ? '0' + Data.getMinutes().toLocaleString() : Data.getMinutes().toLocaleString();
        }, 100);
      },
      selectApp(event) {
        this.app = event;
        if (event == 1 || event == 5) {
          this.timeStyle = "#222";
        } else if (event == 0) this.timeStyle = "#fff"
      },
      drawMenu(event) {
        if (event) this.drawLock = false;

      },
      noteOpen(title){

      },
      createNote(){
        alert(this.createNoteContent + this.createNoteTitle);
      },
      nbutton(key){
        if(key == 'del') this.number = this.number.substring(0, this.number.length - 1);
        else {
          if(this.number.length < 8) this.number += key;
        }
      },
      setWallpaper(id){
        switch (id) {
          case 1:
            this.wallpaper = "styles/img/bg-01.jpg";
            break;
          case 2:
            this.wallpaper = "styles/img/bg-02.jpg";
            break;
          case 3:
            this.wallpaper = "styles/img/bg-03.jpg";
            break;
          case 4:
            this.wallpaper = "styles/img/bg-04.jpg";
            break;
          case 5:
            this.wallpaper = "styles/img/bg-05.jpg";
            break;
          case 6:
            this.wallpaper = "styles/img/bg-06.jpg";
            break;
          case 7:
            this.wallpaper = "styles/img/bg-07.jpg";
            break;
          case 8:
            this.wallpaper = "styles/img/bg-08.jpg";
            break;
        
          default:
            break;
        }
      }
    },
    data: {
      draw: false,
      drawLock: true,
      app: 0,
      appStyle: "#222",
      timeStyle: "#fff",
      wallpaper: "styles/img/bg-01.jpg",
      currentHours: '',
      currentMinutes: '',
      phoneContainerGrid: 'iphone-x',
      phoneDeviceDiv: 'device device-iphone-x',
      noteTitle: [],
      noteContent: [],
      createNoteTitle: '',
      createNoteContent: '',
      number: ''
    }
  })
