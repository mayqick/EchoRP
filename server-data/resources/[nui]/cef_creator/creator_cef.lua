function renderCreatorCef()
    SendNUIMessage({
        type = 'render'
    })
end
function focusCreatorCef()
    SetNuiFocus(true, true)
end
RegisterNUICallback('SendCharacterSettings', function(data, cb)

    print(data)
    TriggerEvent("onCharacterCreatorChangeSettings", data)
  
    cb('ok')
  end)