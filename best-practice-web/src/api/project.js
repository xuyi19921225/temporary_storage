import request from '@/utils/request'

export function getProjectList(params) {
  return request({
    url: '/project',
    method: 'get',
    params
  })
}

export function AddProjectList(data) {
  return request({
    url: '/project',
    method: 'post',
    data
  })
}

export function getProjectByID(params) {
  return request({
    url: '/project/GetProjectByID',
    method: 'get',
    params
  })
}

export function saveProject(data) {
  return request({
    url: '/project',
    method: 'put',
    data
  })
}

export function batchSaveProject(data) {
  return request({
    url: '/project/BatchAdd',
    method: 'post',
    data
  })
}

export function deleteProject(params) {
  return request({
    url: '/project',
    method: 'delete',
    params
  })
}

