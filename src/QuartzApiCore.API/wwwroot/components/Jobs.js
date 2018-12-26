import {connect} from 'react-redux';
import React from 'react';
import {Card, Row} from 'react-bootstrap';
import {FontAwesomeIcon} from '@fortawesome/react-fontawesome';
import {pauseJob, triggerJob} from "../actionCreators";
import dateFormat from 'dateformat';

const Trigger = ({trigger, pauseJob, triggerJob}) => {
    return (
        <div>
            <strong>{trigger.name}</strong>
            <br/>
            <small className="text-muted">Next fire time</small> { dateFormat(trigger.nextFireTime)}
        </div>
    )
};

const Job = ({job, pauseJob, triggerJob}) => (
    <Card>
        <Card.Body>
            <Card.Title>{job.name}</Card.Title>
            <Card.Subtitle className="mb-2 text-muted">{job.group}</Card.Subtitle>
            <Card.Text>
                <h4>Triggers</h4>
                {job.triggers.map(t => (<Trigger trigger={t}/>))}
            </Card.Text>
            <Card.Link>
                <a href="#" onClick={pauseJob.bind(this, job)}>
                <FontAwesomeIcon icon="pause"/>
                </a>
            </Card.Link>
            <Card.Link>
                <a href="#" onClick={triggerJob.bind(this, job)}>
                <FontAwesomeIcon icon="play"/>
                </a>
            </Card.Link>
        </Card.Body>

    </Card>
)

const Jobs = ({match, jobs, ...rest}) => {
    
    return (
        <div>
            <h1>Jobs</h1>
            <Row>
            {jobs.map(job => ( <Job job={job} {...rest} ></Job>))}
            </Row>
        </div>
    );
}


const mapStateToPros = ({jobs}) => ({
    jobs
});

const mapDispatchToProps = (dispatch) =>({
   pauseJob: (job) => dispatch(pauseJob(job)), 
    triggerJob: (job) => dispatch(triggerJob(job))
});

export default connect(mapStateToPros, mapDispatchToProps)(Jobs);