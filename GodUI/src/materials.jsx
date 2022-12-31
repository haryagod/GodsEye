import React, { Component } from 'react';
import {   Grid } from '@mui/material';
import GenericList from './common/list';
import BasicSelect from './common/select';
import httpService from './common/httpService';
import S3upload from './common/s3upload';
import S3StreamVedio from './common/s3StreamVedio';
export default class Materials extends Component {


  state ={
   Id:'',
   Name:'',   
   selectedStandard:'',
   selectedSubject:'',
   selectedChapter:'',
   docList:[],
   standards:[],
   subjects:[],
   chapters:[]
  }
  componentDidMount(){
    this.getStandards();     
  }
  handleStandardChange = e =>{
      const selectedStandard = e.target.value;
   const subjects = this.state.standards.filter(e=>e.id === selectedStandard)[0].Subjects;   
 this.setState({selectedStandard,selectedChapter:'',selectedSubject:'',subjects});
  } 

  handleSubjectChange = e =>{
    const selectedSubject = e.target.value;
 const chapters = this.state.subjects.filter(e=>e.id === selectedSubject)[0].chapters;   
this.setState({selectedChapter:'',selectedSubject,chapters});
} 
handleChapterChange = e =>{
    const selectedChapter = e.target.value;
   this.setState({selectedChapter});
   this.props.changeAwsDir(this.state.selectedStandard+'/'+this.state.selectedSubject+'/'+selectedChapter) 
}

 getStandards=async () =>{
 const {data:standards} = await httpService.get('https://localhost:44306/api/Standard');
 this.setState({standards});
 }
 
 vedioCompKey=0;
  render (){
    
  this.vedioCompKey++;
      const {standards,subjects,chapters} =this.state
   return <Grid container spacing={2}>
    <Grid item xs={3}>
    <BasicSelect label="Standard" menuItemList={standards} onChange={this.handleStandardChange} value={this.state.selectedStandard} />
   {this.state.selectedStandard && <BasicSelect label="Subject" onChange={this.handleSubjectChange} menuItemList={subjects} value={this.state.selectedSubject}  />}
   {this.state.selectedSubject && <BasicSelect label="Chapter" onChange={this.handleChapterChange} menuItemList={chapters} value={this.state.selectedChapter} />}
   {this.state.selectedChapter &&  
   <GenericList 
   awsFileList={this.props.awsConfig.awsFileList}
    onDelete={this.props.onDelete} 
    onAwsFileList={this.props.onAwsFileList} 
     onVedioSelect ={this.props.onVedioSelect}
      />}
    </Grid>
    <Grid item xs={9}>
     <div>

      <div>
     {/* <BucketList onDelete={this.props.onDelete} listFiles={this.props.awsFileList} 
     onAwsFileList={this.props.onAwsFileList} 
     onVedioSelect ={this.props.onVedioSelect}/> */}
     <S3upload onUpload ={this.props.onUpload}></S3upload>
    {this.props.awsConfig.selectedVedio !='' && (<S3StreamVedio key={this.vedioCompKey}  vedioLink={this.props.awsConfig.selectedVedio}  />)}
       </div>

     </div>
    </Grid>
  </Grid>
   
  };
}
