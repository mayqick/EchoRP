function renderAuthCef()
    SendNUIMessage({
        type = "render"
    })
end
function focusAuthCef()
    SetNuiFocus(true, true)
end

RegisterNUICallback('sendMail', function(data, cb)
    local mail = data.mail
    TriggerServerEvent("onPlayerRegistration", mail)
  
    cb('ok')
  end)
