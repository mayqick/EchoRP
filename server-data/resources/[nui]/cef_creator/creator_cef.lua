function renderCreatorCef()
    SendNUIMessage({
        type = 'render'
    })
end
function focusCreatorCef()
    SetNuiFocus(true, true)
end
RegisterNUICallback('SendCharacterSettings', function(data, cb)

    TriggerEvent("onCharacterCreatorChangeSettings", data)
  
    cb('ok')
  end)
RegisterNUICallback('SaveCharacter', function(data, cb)

    TriggerEvent("onSavePlayerCharacter", data)
  
    cb('ok')
  end)
RegisterNUICallback('SendCharacterGender', function(data, cb)

    TriggerEvent("changePlayerFreemode", data)
  
    cb('ok')
  end)