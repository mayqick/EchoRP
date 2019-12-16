let register = false;
function registerAccount() {
  register = true;
  document.getElementById('recovery').style.display = 'none';
  document.getElementById('reg_main').style.display = 'none';
  document.getElementById('center__items').style.marginLeft = "-200px";

  document.getElementById('back_arrow').style.display = 'block';
  document.getElementById('password_again').style.display = 'block';
  document.getElementById('promo').style.display = 'block';
  document.getElementById('auth_btn').textContent = "Зарегистрироваться";
  document.getElementById('reg').textContent = "Регистрация";
  document.getElementById('info').textContent = "Запомните данные которые вводите, они потребуются для авторизации.";
}


function authAccount() {
  if (!register) {
    let password = document.getElementById('password').value;
    let login = document.getElementById('login').value;
    mp.trigger('loadPlayerAccount', login, password);
  }
  else {
    let login = document.getElementById('login').value;
    let password = document.getElementById('password').value;
    let password_retry = document.getElementById('password_again').value;
    let promo = document.getElementById('promo').value;

    var nick = /[A-Za-z0-9_]{2,32}/;
    var reg = /[A-Za-z0-9_]{6,32}/;

    if (!nick.test(login)) {
      info.style.color = "red";
      info.textContent = "Логин некорректен!\nМожно использовать символы латинского алфавита, от 6 до 32, цифры и символ подчёркивания!";
      return;
    }

    if (!reg.test(password)) {
      info.style.color = "red";
      info.textContent = "Пароль введён некорректно!\nМожно использовать символы латинского алфавита, от 6 до 32, цифры и символ подчёркивания";
      return;
    }

    if (password != password_retry) {
      info.style.color = "red";
      info.textContent = "Введённый вами пароли не совпадают!";
      return;
    }

    mp.trigger('createPlayerAccount', login, password, promo);
  }
}
function backToAuth() {
  register = false;
  document.getElementById('recovery').style.display = 'block';
  document.getElementById('reg_main').style.display = 'block';
  document.getElementById('center__items').style.marginLeft = "-400px";

  document.getElementById('back_arrow').style.display = 'none';
  document.getElementById('password_again').style.display = 'none';
  document.getElementById('promo').style.display = 'none';
  document.getElementById('auth_btn').textContent = "Войти";
  document.getElementById('reg').textContent = "Авторизация";
  document.getElementById('info').textContent = "Чтобы войти на аккаунт, заполните поля, которые вы указывали при регистрации.";
}
function getRandomInt(min, max) {
  return Math.floor(Math.random() * (max - min)) + min;
}

function showReg() {
  mp.trigger('registerPage');
}
function registerError(){
  info.style.color = "red";
  info.textContent = "Аккаунт с таким логином уже зарегистрирован!\nПожалуйста, введите другой логин.";
}
function showError() {
  document.getElementById('error').style.visibility = 'visible';
  document.getElementById('error').style.position = 'static';
}
