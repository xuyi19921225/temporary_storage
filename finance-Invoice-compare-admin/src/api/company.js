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

export function getCompanyById(params) {
  return request({
    url: '/Company/getCompanyById',
    method: 'get',
    params
  })
}

export function getCompanyList(params) {
  return request({
    url: '/company',
    method: 'get',
    params
  })
}

export function addCompany(data) {
  return request({
    url: '/company',
    method: 'post',
    data
  })
}

export function saveCompany(data) {
  return request({
    url: '/company',
    method: 'put',
    data
  })
}

export function deleteCompany(params) {
  return request({
    url: '/company',
    method: 'delete',
    params
  })
}

