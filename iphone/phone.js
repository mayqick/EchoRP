Data = new Date()
var devicesApp = new Vue({
    el: '#content',
    methods: {
      phone: function (event) {
        // `this` внутри методов указывает на экземпляр Vue
        if (event) this.draw = true
      },
      selectApp: function(event) {
        this.app = event
        if (event == 1) {
          this.appStyle = "#fff"
          this.timeStyle = "#222"
        }
      },
      drawMenu: function(event) {
        if (event) this.drawLock = false

      }
    },
    data: {
      draw: true,
      drawLock: true,
      app: 0,
      appStyle: "#222",
      timeStyle: "#fff",
      currentHours: Data.getHours().toLocaleString(),
      currentMinutes: Data.getMinutes() < 10 ? '0' + Data.getMinutes().toLocaleString() : Data.getMinutes().toLocaleString(),
      phoneContainerGrid: 'iphone-x',
      phoneDeviceDiv: 'device device-iphone-x'
    }
  })
