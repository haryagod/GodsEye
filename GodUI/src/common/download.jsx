import * as React from 'react';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import Divider from '@mui/material/Divider';
import ListItemText from '@mui/material/ListItemText';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import Avatar from '@mui/material/Avatar';
import { Button, Card, CardActions, CardContent, Container, Icon, ListItemIcon } from '@mui/material';
import ProgressBar from './progressbar';
import FileUploadIcon from '@mui/icons-material/FileUpload';

const Downloads = ({onResetDownloadList,progressList}) => {

  const changeColor=(number)=>
  {
    if(number==100)
    return 'success'
   if(number >=0  && number <=10 
    || number >=20  && number <=30
    || number >=40  && number <=50
    || number >=60  && number <=70
    || number >=80  && number <=90
     )
     return 'secondary';
     return 'primary';
  }
    return (

        < Box style={{position: 'absolute',flex:1, left: '70%', right: 0, bottom: 0}}>
     <Container >
        <Card >
        <CardContent >
        <Typography sx={{ fontSize: 14 }}  component={'span'} color="#f50057">
          Uploads
        </Typography>
    <List sx={{ width: '100%', maxWidth: 360, bgcolor: 'background.paper' }}>
      {progressList.map((progress,index)=>{
        return <React.Fragment key={index}>
         <ListItem alignItems="flex-start">
         <ListItemIcon>
           <FileUploadIcon color={changeColor(progress.loaded/progress.total*100) } />
         </ListItemIcon>
         <React.Fragment>
               <ProgressBar 
                 label={progress.key.split('/').pop()}
                 progress ={progress.loaded/progress.total*100||0}
                 sx={{ display: 'inline' }}
                 component="span"
                 variant="body2"
                 color="text.primary"
               />
     
             </React.Fragment>
       </ListItem>
       <Divider variant="inset" component="li" />
       </React.Fragment>
      }
        
    )}
      
    </List>
    </CardContent>
    <CardActions>
        <Button onClick={()=>onResetDownloadList()} size="small">dismiss</Button>
      </CardActions>
    </Card>
    </Container>
        </Box>
    
         
         );
}
 
export default Downloads;