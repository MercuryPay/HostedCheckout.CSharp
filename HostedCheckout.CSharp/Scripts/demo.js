//sets focus to the control specified by controlName
function SetFocus(controlName) {

    //debugger;
    var id = GetClientId(controlName);
    if (id) {
        inputCtl = document.getElementById(id);
        if ((inputCtl != null) && (!inputCtl.disabled)) {
            inputCtl.focus();
            setCursorToTextEnd(inputCtl);
        }
    }
}

//focus helper function
function controlFocus(objId) {
    var ctl = document.getElementById(objId);
    ctl.focus();
    setCursorToTextEnd(ctl);
}


function GetClientId(strid) {
    var count = document.getElementsByTagName('*').length; //<-- gets all elements, instead of Forms as this only returns FORM elements
    var i = 0;
    var eleName;
    for (i = 0; i < count; i++) {
        eleName = document.getElementsByTagName('*')[i].id;
        pos = eleName.indexOf(strid);
        if (pos >= 0) break;
    }
    return eleName;
}

//Sets cursor to end of text in textbox
function setCursorToTextEnd(ctl) {
    if (ctl.createTextRange) {
        var FieldRange = ctl.createTextRange();
        FieldRange.moveStart('character', ctl.value.length);
        FieldRange.collapse();
        FieldRange.select();
    }
}

function toggle_visibility(id, lbtnID) {
    
    var e = document.getElementById(id);
    var clientID = GetClientId(lbtnID);
    var ctl = document.getElementById(clientID);
    if (e.style.display == 'inherit') 
    {
        e.style.display = 'none';

        if (ctl) {
            if (ctl.innerHTML == 'Hide Initialize Fields')
                ctl.innerHTML = 'Show Initialize Fields';
            if (ctl.innerHTML == 'Hide Verification Fields')
                ctl.innerHTML = 'Show Verification Fields';
            if (ctl.innerHTML == 'Hide Billing Info')
                ctl.innerHTML = 'Show Billing Info';

        }
    }
    else {
        e.style.display = 'inherit';
        e.setAttribute('style', 'display:inherit');
        if (ctl) {
            if (ctl.innerHTML == 'Show Initialize Fields')
                ctl.innerHTML = 'Hide Initialize Fields';
            if (ctl.innerHTML == 'Show Verification Fields')
                ctl.innerHTML = 'Hide Verification Fields';
            if (ctl.innerHTML == 'Show Billing Info')
                ctl.innerHTML = 'Hide Billing Info';
        }
    }
 }


 function CheckForOther(radioID, divName) {
     
     if (GetRadioSelectedValue(radioID) == "Other") 
         document.getElementById(divName).style.display = 'inherit';
     else
         document.getElementById(divName).style.display = 'none';
       
  }

  function CheckForVoiceAuth() {
      ctl = document.getElementById('ctl00_MainContent_rblTranType_2');
      if (ctl) {
          if (ctl.checked) {
              document.getElementById('divVoiceAuth1').style.display = 'block';              
              document.getElementById('divVoiceAuth2').style.display = 'inherit';
          }
          else {
              document.getElementById('divVoiceAuth1').style.display = 'none';
              document.getElementById('divVoiceAuth2').style.display = 'none';
          }
      }
  }

  function GetRadioSelectedValue(name) {

      //get the radio buttons and find out which one has been selected...
      var id = GetClientId(name);
      if (id) {
          var container = document.getElementById(id);
          var radioButtons = container.getElementsByTagName('INPUT');
          var checkedIndex = null;

          for (var i = 0; i < radioButtons.length; i++) {
              if (radioButtons[i].checked) {
                  return radioButtons[i].value;
              }
          }
      }
      return "";
  }

