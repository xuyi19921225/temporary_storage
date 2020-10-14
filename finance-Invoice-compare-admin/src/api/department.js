import request from '@/utils/request'

export function getDepartmentList(params) {
  return request({
    url: '/Department',
    method: 'get',
    params
  })
}
