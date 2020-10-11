<template>
  <div class="app-container">
    <el-form
      ref="form"
      :model="form"
      :rules="rules"
      label-width="120px"
      class="demo-ruleForm"
    >
      <el-row>
        <el-col :span="8">
          <el-form-item
            label="Site"
            prop="siteID"
          >
            <el-select
              v-model="form.siteID"
              filterable
              clearable
              disabled
              style="width: 100%;"
              @change="getSiteAreaList({parentID:form.siteID,module:'division'})"
            >
              <el-option
                v-for="item in siteList"
                :key="item.id"
                :label="item.value"
                :value="item.id"
              />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item
            label="Division"
            prop="divisionID"
          >
            <el-select
              v-model="form.divisionID"
              filterable
              clearable
              disabled
              style="width: 100%;"
              @change="getSiteAreaList({parentID:form.divisionID,module:'station'})"
            >
              <el-option
                v-for="item in divisionList"
                :key="item.id"
                :label="item.value"
                :value="item.id"
              />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item
            label="Station"
            prop="stationID"
          >
            <el-select
              v-model="form.stationID"
              filterable
              clearable
              style="width: 100%;"
            >
              <el-option
                v-for="item in stationList"
                :key="item.id"
                :label="item.value"
                :value="item.id"
              />
            </el-select>
          </el-form-item>
        </el-col>
      </el-row>

      <el-row>
        <el-col :span="8">
          <el-form-item
            label="部门"
            prop="department"
          >
            <el-input v-model="form.department" />
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item
            label="项目标题"
            prop="projectTitle"
          >
            <el-input v-model="form.projectTitle" />
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item
            label="描述"
            prop="description"
          >
            <el-input v-model="form.description" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="24">
          <el-form-item
            label="附件"
            prop="attachment"
          >
            <el-upload
              class="upload-demo"
              drag
              action="#"
              :http-request="upload"
              :on-remove="onRemove"
              :file-list="fileList"
              accept=".docx,.ppt,.pdf"
            >
              <i class="el-icon-upload" />
              <div class="el-upload__text">将文件拖到此处，或<em>点击上传</em></div>
              <div
                slot="tip"
                class="el-upload__tip"
              >只能上传Word,PDF,PPT文件</div>
            </el-upload>
          </el-form-item>
        </el-col>
      </el-row>
      <el-form-item>
        <el-button
          type="primary"
          @click="onSubmit"
        >保存</el-button>
        <el-button @click="onCancel">退出</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import { getSiteAreaListByParameter } from '@/api/sitearea'
import { fileUpload } from '@/api/upload'
import { saveProject, getProjectByID } from '@/api/project'

export default {
  name: 'UserCreate',
  data() {
    return {
      form: {
        id: '',
        department: '',
        divisionID: '',
        siteID: '',
        stationID: '',
        projectTitle: '',
        description: '',
        filename: '',
        attachment: ''
      },
      fileList: [],
      siteList: [],
      divisionList: [],
      stationList: [],
      rules: {
        department: [
          { required: true, message: '部门不能为空', trigger: ['blur', 'change'] }
        ],
        divisionID: [
          { required: true, message: 'Division不能为空', trigger: ['blur', 'change'] }
        ],
        siteID: [
          { required: true, message: 'Site不能为空', trigger: ['blur', 'change'] }
        ],
        stationID: [
          { required: true, message: 'Sation不能为空', trigger: ['blur', 'change'] }
        ],
        projectTitle: [
          { required: true, message: '项目标题不能为空', trigger: ['blur', 'change'] }
        ]
      }
    }
  },
  created() {
    this.getProjectByID()
    this.getSiteAreaList({ parentID: -1 })
    this.getSiteAreaList({ parentID: this.$route.query.siteID, module: 'division' })
    this.getSiteAreaList({ parentID: this.$route.query.divisionID, module: 'station' })
  },
  methods: {
    async getSiteAreaList({ parentID, module = 'site' }) {
      await getSiteAreaListByParameter({ parentID: parentID }).then(res => {
        switch (module) {
          case 'site':
            this.siteList = res.response
            break
          case 'division':
            this.divisionList = res.response
            break
          case 'station':
            this.stationList = res.response
            break
          default:
            break
        }
      })
    },
    updateProject() {
      this.form.updatedBy = this.$store.getters.name
      saveProject(this.form).then(res => {
        if (res.success === true) {
          this.$message('更新成功')
        } else {
          this.$message.error('更新失败')
        }
      })
    },
    getProjectByID() {
      this.id = this.$route.query.id
      getProjectByID({ id: this.id }).then(res => {
        if (res.response) {
          this.setInfo(res.response)
        }
      })
    },
    setInfo(row) {
      this.form.id = row.id
      this.form.siteID = row.siteID
      this.form.divisionID = row.divisionID
      this.form.stationID = row.stationID
      this.form.department = row.department
      this.form.projectTitle = row.projectTitle
      this.form.description = row.description
      this.form.filename = row.fileName
      this.form.attachment = row.attachment
      if (row.filename !== '' && row.attachment !== '') {
        this.fileList = [{ name: row.fileName, url: row.attachment }]
      }
    },
    upload(params) {
      const file = params.file

      // 根据后台需求数据格式
      const form = new FormData()
      // 文件对象
      form.append('file', file)

      // 项目封装的请求方法，下面做简单介绍
      fileUpload(form)
        .then(res => {
          if (res.success === true) {
            this.$message('上传成功')
            this.form.filename = res.response.fileName
            this.form.attachment = res.response.url
            this.fileList = [{ name: this.form.filename, url: this.form.url }]
          } else {
            this.$message(res.message)
          }
        })
    },
    beforeUpload(file) {
      const fileType = file.type
      const fileTypeArr = ['application/vnd.openxmlformats-officedocument.wordprocessingml.document']
      const isFileType = fileTypeArr.indexOf(fileType) !== -1
      const isLt4M = file.size / 1024 / 1024 < 4

      if (!isFileType) {
        this.$message.error('只能上传docx文件格式!')
        return false
      }
      if (!isLt4M) {
        this.$message.error('只能上传Word文件大小小于4M')
        return false
      }
      return true
    },
    onRemove(file, fileList) {
      if (file && file.status === 'success') {
        this.form.filename = ''
        this.form.attachment = ''
        this.fileList = []
      }
    },
    onSubmit() {
      this.$refs['form'].validate((valid) => {
        if (valid) {
          this.updateProject()
        } else {
          console.log('error submit!!')
          return false
        }
      })
    },
    onCancel() {
      history.back()
    }
  }
}
</script>

<style scoped>
.line {
  text-align: center;
}

</style>

