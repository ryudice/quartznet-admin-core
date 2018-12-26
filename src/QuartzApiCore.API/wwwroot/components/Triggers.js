import {connect} from 'react-redux';
import React from 'react';

const Triggers = ({match,jobs})=>{

    return (
        <div>
            <h1>Jobs Index</h1>
            {jobs.map( job=> (<Job job={job}></Job>))}
        </div>
    );
}
