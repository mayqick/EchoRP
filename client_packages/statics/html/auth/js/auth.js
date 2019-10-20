
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
        dialog_animation: false,
        email: '',
        emailError: false,
        code_num1: '',
        code_num2: '',
        code_num3: '',
        code_num4: '',
        errorMessage: ''
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
        },
        mailTest(email){
            var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return re.test(email);
        },
        checkMailCode(){
            var code = this.code_num1 + this.code_num2 + this.code_num3 + this.code_num4;
        
            mp.trigger('checkCode', code);
        },
        register(){
            if(this.mailTest(this.email)){
                this.show = 2;
                mp.trigger('mailVerification', this.email);
                this.emailError = false;
            } 
            else {
                this.showError(1);
            }
        },
        showError(type){
            if (type == 1){
                this.errorMessage = "Вы ввели некорректный адрес электронной почты!";
                this.emailError = true;
            }
            else if (type == 2){
                this.errorMessage = 'Неверный проверочный код.'; 
                this.emailError = true;
            }
        }
    }
})
