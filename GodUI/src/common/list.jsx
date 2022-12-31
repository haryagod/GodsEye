

import * as React from 'react';
import ListSubheader from '@mui/material/ListSubheader';
import List from '@mui/material/List';
import Card from '@mui/material/Card';
import Container from '@mui/material/Container';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemText from '@mui/material/ListItemText';

class GenericList extends React.Component {
  
  componentDidMount(){
    this.props.onAwsFileList();
    }
   render() {
   const {awsFileList,onDelete,onVedioSelect}= this.props
   console.log(awsFileList,"fileList")
    return (
      
      <Container  sx={{bgcolor: 'primary' }}>
    <List
      //   sx={{ width: '100%', overflow:'auto',maxWidth: 260,maxHeight:'200', bgcolor: 'background.paper' }}
      sx={{
        
          bgcolor: 'background.paper',
          position: 'relative',
          overflow: 'auto',
          maxHeight: 600,
          display: { xs: 'none', sm: 'block' },
          '& ul': { padding: 0 },
        }}
      component="nav"
        aria-labelledby="nested-list-subheader"
        subheader={
          <ListSubheader component="div" id="nested-list-subheader">
            Learning Materials
          </ListSubheader>
        }
      >

                
        {awsFileList.map(document=>{
          
        return <ListItemButton key={document.Key} onClick={()=>onVedioSelect(document.Key)}>
        <ListItemText primary={document.Key.split("/").pop()} />
      </ListItemButton> 
        })} 
      </List>
    </Container>
      )
  }
  
  }

 
export default GenericList;

    