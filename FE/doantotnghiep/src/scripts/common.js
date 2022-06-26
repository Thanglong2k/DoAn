var Common = Common || {};
Common.formatMoney = (money) => {
  return money ? money.split('').reverse().reduce((prev, next, index) => {
    return ((index % 3) ? next : (next + '.')) + prev
  }) + " VNĐ" : ''
}
Common.formatMoney2 = (money) => {
  return money.replace(/\B(?=(\d{3})+(?!\d))/g, '.')
}
Common.formatDate = (date, format) => {
  const yyyy = date.getFullYear();
  let MM = date.getMonth() + 1; // Months start at 0!
  let dd = date.getDate();
  let hh = date.getHours();
  let mm = date.getMinutes();

  if (dd < 10) dd = '0' + dd;
  if (MM < 10) MM = '0' + MM;
  if (hh < 10) hh = '0' + hh;
  if (mm < 10) mm = '0' + mm;
  let dateReturn=''
  if(format==='dd/MM/yyyy'){
    dateReturn=dd + '/' + MM + '/' + yyyy
  }else if(format==='MM/yyyy'){
    dateReturn=MM + '/' + yyyy
  }else if(format==='dd/MM/yyyy hh:mm'){
    dateReturn=dd + '/' + MM + '/' + yyyy + ' ' + hh + ':' + mm
  }
  return dateReturn;
}
Common.executeFunctionByName = (functionName, context /*, args */ ) => {
  var namespaces = functionName.split(".");
  for (var i = 0; i < namespaces.length; i++) {
    context = context[namespaces[i]];
  }
  return context;
}
Common.takeTheFirstLetter = (text) => {
  var textArray = text.split(" ");
  let context = ""
  for (var i = 0; i < textArray.length; i++) {
    context += textArray[i].charAt(0).toUpperCase();
  }
  return context
}
Common.createGuid = () => {
  function S4() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
  }
  return (S4() + S4() + "-" + S4() + "-4" + S4().substr(0, 3) + "-" + S4() + "-" + S4() + S4() + S4()).toLowerCase();
}
Common.setCommentTime = (date) => {
  const t = new Date() - new Date(date)
  let text = ''
  console.log(t);
  if (t < 1000 * 60) {
    text = 'Vừa xong'
  } else if (t < 1000 * 60 * 60) {
    text = (Math.round(t / (1000 * 60))) + " phút trước"
  } else if (t < 1000 * 60 * 60 * 24) {
    text = (Math.round(t / (1000 * 60 * 60))) + " giờ trước"
  } else if (t < 1000 * 60 * 60 * 24 * 7) {
    text = (Math.round(t / (1000 * 60 * 60 * 24))) + " ngày trước"
  } else if (t < 2629746000) {
    text = (Math.round(t / (1000 * 60 * 60 * 24 * 7))) + " tuần trước"
  } else if (t < 31556952000) {
    text = (Math.round(t / 2629746000)) + " tháng trước"
  } else {
    text = (Math.round(t / 31556952000)) + " năm trước"
  }

  return text

}
Common.showMark = (container) => {
  var option = document.createElement("div");
  option.className = "loader-container"
  var htmlElement = `
  <div class="loader">
  
      </div>
  `
  option.innerHTML += htmlElement
  var t = document.querySelector(`.${container}`)
  if (!container || !t) {
    t = document.querySelector("body")
  }
  t.classList.add("tw-relative")
  t.appendChild(option)
}
Common.hideMark = (container) => {
  var option = document.querySelector(".loader-container");

  var t = document.querySelector(`.${container}`)
  if (!container || !t) {
    t = document.querySelector("body")
  }
  t.removeChild(option)
}
Common.unique = (arr) => {
  return Array.from(new Set(arr)) //
}
Common.addHours=(numOfHours, date) => {
  date.setTime(date.getTime() + numOfHours * 60 * 60 * 1000);

  return date;
}
export default Common;