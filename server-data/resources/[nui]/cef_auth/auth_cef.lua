function renderAuthCef()
    SendNUIMessage({
        type = "render"
    })
end
function focusAuthCef()
    SetNuiFocus(true, true)
end

RegisterNUICallback('sendMail', function(data, cb)

    TriggerServerEvent("onPlayerRegistration", data.mail)
  
    cb('ok')
end)

  RegisterNUICallback('sendMailCode', function(data, cb)
    TriggerEvent("checkPlayerRegisterCode", data.code)
  
    cb('ok')
  end)