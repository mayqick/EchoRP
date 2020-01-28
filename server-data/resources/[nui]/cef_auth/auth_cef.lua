function renderAuthCef(render)
    if render then
      SendNUIMessage({
          type = 'render'
      })
    else 
      SendNUIMessage({
        type = 'unrender'
    })
    end
  end
  
  function focusAuthCef(focus)
    if focus then
      SetNuiFocus(true, true)
    else
      SetNuiFocus(false, false)
    end
  end
  

RegisterNUICallback('sendMail', function(data, cb)

    TriggerServerEvent("onPlayerRegistration", data.mail)
  
    cb('ok')
end)

  RegisterNUICallback('sendMailCode', function(data, cb)
    TriggerEvent("checkPlayerRegisterCode", data.code)
  
    cb('ok')
  end)