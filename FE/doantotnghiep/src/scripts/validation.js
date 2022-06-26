import Resource from '../scripts/resource';
var Validation = Validation || {};
Validation.Response = {
    isError: true,
    errorText: ''
  }
/**
 * check dữ liệu email
 * CreatedBy: TTLONG (20/07/2021)
 */
Validation.email=(value)=>{
    let regex = /^([a-zA-Z0-9_.\-+])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(value);
}
/**
 * check dữ liệu phoneNumber
 * CreatedBy: TTLONG (20/07/2021)
 */
Validation.phoneNumber=(value)=>{
    let regex = /^(0|(\+84)){1}(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$/; //0328564125,+84328564125
    return regex.test(value);
}
/**
 * check dữ liệu identityNumber
 * CreatedBy: TTLONG (20/07/2021)
 */
Validation.identityNumber=(value)=>{
    let regex = /^([0-9]{9}|[0-9]{12})$/;
    return regex.test(value);
}

/**
 * check dữ liệu theo regex
 * CreatedBy: TTLONG (20/07/2021)
 */
Validation.validateData=(checRegex, label) =>{
    let result={...Validation.Response};
    if (!checRegex) {
        result.isError= true;
        result.errorText = Resource.MsgResponse.MsgAlertFail.format(label);
    } else {
      result.isError = false;
      result.errorText = "";
    }
    return result;
}
/**
 * check dữ liệu
 * CreatedBy: TTLONG (20/07/2021)
 */
Validation.validate=(type,value,label)=>{
    let checkRegex=true;
    switch(type){
        case "email":
            checkRegex=Validation.email(value);
            break;
        case "phoneNumber":
            checkRegex=Validation.phoneNumber(value);
            break;
        case "identityNumber":
            checkRegex=Validation.identityNumber(value);
            break;
    }
    return Validation.validateData(checkRegex,label);
    
}
export default Validation;