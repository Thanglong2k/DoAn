//Resource dùng chung cho toàn chương trình
var Resource = Resource || {};

Resource.MsgQuestion = {
    MsgConfirmDeleteTeacher: "Bạn có chắc chắn muốn xóa cán bộ/giáo viên này không?",
    MsgConfirmDeleteCombinationSubject: "Bạn có chắc chắn muốn xóa tổ - bộ môn này không?",
    MsgConfirmDataChanged: "Dữ liệu đã bị thay đổi, bạn có muốn cất không?",
}
Resource.MsgResponse = {
    MsgAlertError: "{0} không được để trống",
    MsgAlertFail: "{0} không đúng định dạng",
    MsgAlertDateExceed: "Ngày bạn chọn vượt quá ngày hiện tại",
    MsgToastAddSuccess:"Thêm thành công",
    MsgToastEditSuccess:"Sửa thành công",
    MsgToastDeleteSuccess:"Xóa thành công",
    MsgToastLoadDateSuccess:"Tải dữ liệu thành công",
    MsgToastException:"Lỗi! Vui lòng liên hệ MISA",
    MsgWarningCombinationSubject:"Bạn không thể xóa tổ - bộ môn đã phát sinh dữ liệu liên quan. Vui lòng kiểm tra lại",
    MsgAlertDataInValid:"Dữ liệu không hợp lệ. Vui lòng kiểm tra lại"
}
Resource.MsgDuplicate={
    MsgTeacherCode:"Số hiệu cán bộ giáo viên đã tồn tại trong danh sách. Vui lòng kiểm tra lại",
    MsgTooltipTeacherCode:"Số hiệu cán bộ giáo viên đã tồn tại",
    MsgTeacherName:"Tên cán bộ giáo viên đã tồn tại trong danh sách. Vui lòng kiểm tra lại",
    MsgCombinationSubjectName:"Tên tổ bộ đã tồn tại trong danh sách. Vui lòng kiểm tra lại",
    MsgTooltipCombinationSubjectName:"Tên tổ bộ đã tồn tại",
}
Resource.Toast={
    ToastType:{
        ToastSuccess:"success",
        ToastFail:"error",
    },
    ToastTitle:{
        ToastSuccess:"Thành công"
    },
    ToastText:{
        ToastSuccessDeleteTeacher:"Xóa cán bộ/giáo viên thành công.",
        ToastSuccessDeleteCombinationSubject:"Xóa tổ - bộ môn thành công.",
        ToastSuccessInsertTeacher:"Thêm cán bộ/giáo viên thành công.",
        ToastSuccessInsertCombinationSubject:"Thêm tổ - bộ môn thành công.",
        ToastSuccessEditTeacher:"Sửa cán bộ/giáo viên thành công.",
        ToastSuccessEditCombinationSubject:"Sửa tổ - bộ môn thành công.",
    }
    

}
Resource.DialogType={
    Teacher:"teacher",
    CombinationSubject:"combinationSubject",

}
Resource.MisaCode={
    CodeSuccess:"MISA-200",
    CodeException:"MISA-500",
    CodeEmpty:"MISA-001",
    CodeDuplicate:"MISA-002",
    CodeInvalid:"MISA-004"
}
export default Resource;