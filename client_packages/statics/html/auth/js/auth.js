
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
        register(){
            if(this.mailTest(this.email)){
                this.show = 2;
                mp.events.call('mailVerification', this.email);
                this.emailError = false;
            } 
            else {
                this.errorMessage = "Вы ввели некорректный адрес электронной почты!";
                this.emailError = true;
            }
            
        },
        mailTest(email){
            var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return re.test(email);
        },
        checkMailCode(){
            var code = code_num1.toString() +  code_num2.toString() + code_num3.toString() + code_num4.toString();
            mp.events.call('checkCode', code);
        }
    }
})
