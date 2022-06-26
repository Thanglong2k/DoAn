import BaseAPIConfig from "../base/BaseAPIConfig";




export default class BaseAPI {
  constructor() {
    this.controller = null;
    this.api=BaseAPIConfig;
  }
  /**
   * Phương thức lấy tất cả dữ liệu
   */
  getAll() {
    return BaseAPIConfig.get(`${this.controller}`);
  }

  /**
   * Phương thức lấy dữ liệu của 1 phần tử
   */
  getById(id) {
    return BaseAPIConfig.get(`${this.controller}/${id}`);
  }

  /**
   * Hàm cập nhật dữ liệu
   * @param {*} id
   * @param {*} body
   */
  update(id, body) {
    return BaseAPIConfig.put(`${this.controller}/id?id=${id}`, body);
  }

  /**
   * Hàm xóa bản ghi
   * @param {*} id
   */
  delete(id) {
    return BaseAPIConfig.delete(`${this.controller}/${id}`);
  }
   /**
   * Hàm xóa nhiều bản ghi
   * @param {*} ids
   */
  deletes(ids) {
    return BaseAPIConfig.delete(`${this.controller}/ids`,{data:ids});//{params:{ids:ids}}: viết lên uri: controller/ids?ids[]=""$ids[]=""
  }

  /**
   * Hàm thêm bản ghi
   * @param {*} body
   */
  insert(body) {
    return BaseAPIConfig.post(`${this.controller}`, body);
  }

  /**
   * Get List paging
   * TTLONG 21/02/2022
   */
  getPaging(params) {
    let urlFull = this.controller + "/paging";
    return BaseAPIConfig.get(urlFull,{params:params});
  }
  
}