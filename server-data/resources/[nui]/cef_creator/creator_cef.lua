function renderCreatorCef(render)
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

function focusCreatorCef(focus)
  if focus then
    SetNuiFocus(true, true)
  else
    SetNuiFocus(false, false)
  end
end

RegisterNUICallback('SendCharacterSettings', function(data, cb)

    TriggerEvent("onCharacterCreatorChangeSettings", data)
  
    cb('ok')
  end)
RegisterNUICallback('UpdateCustomizationCamSettings', function(data, cb)

    TriggerEvent("onUpdateCusomizationCamSettings", data)
  
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