
Vue.directive('focus', {
    // Когда привязанный элемент вставлен в DOM...
    inserted: function (el) {
        // Переключаем фокус на элемент
        el.focus()
    }
})

var app = new Vue({
    el: '#main',
    data: {
        show: 0,
        blurType: 'none',
        dialog_animation: false
    },
    methods: {
        renderType(data) {

            if (data == 1) {
                this.show = 1;
                this.blurType = 'none';
            }
            else if (data == 2) {
                this.show = 2;

            }
            else if (data == 3) {
                this.show = 3;
                this.blurType = 'blur(40px)';
                this.dialog_animation = true
            }
            else if (data == 4) {
                this.show = 4;
            }
            else if (data == 5) {
                this.show = 5;
            }
        }
    }
})
