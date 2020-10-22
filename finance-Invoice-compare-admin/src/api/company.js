import request from '@/utils/request'

export function getAllCompanyList(params) {
  return request({
    url: '/company/getAllCompanyList',
    method: 'get',
    params
  })
}

export function saveUCompany(data) {
  return request({
    url: '/userCompany',
    method: 'post',
    data
  })
}

export function getUCompanyByUid(params) {
  return request({
    url: '/userCompany',
    method: 'get',
    params
  })
}

