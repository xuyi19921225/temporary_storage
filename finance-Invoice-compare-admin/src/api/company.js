import request from '@/utils/request'

export function getAllCompanyList(params) {
  return request({
    url: '/company/getAllCompanyList',
    method: 'get',
    params
  })
}
