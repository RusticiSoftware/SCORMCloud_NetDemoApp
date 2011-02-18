<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HostedReport.aspx.cs" Inherits="HostedDemoApp.HostedReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>Hosted Report</title>


<script type="text/javascript" src="<%=ResolveUrl("~/scripts/jquery-1.3.1.min.js")%>"></script>

<script type="text/javascript" >

$(document).ready(
        function() {
            // Using straight .getScript rather than the $.getJSON because we
            // can't dynamically generate the function name... it has to be
            // hard-coded so we can generate a valid signature. Furthermore, the
            // jQuery.getScript method cannot be used because it, along with
            // most jQuery calls sends an additional _12323232 timestamp type
            // parameter to make it unique for caching purposes.
            //jQuery.getScript("<%=dataUrl%>");
            $.ajax({
                url: "<%=dataUrl%>", 
                dataType: "script",
                type: "GET", 
                cache: true, 
                callback: null, // The DataUrl includes a json callback method of getRegistrationResultCallback()
                data:null});
        }
    );

function getRegistrationResultCallback(data) {
    render(data.rsp.registrationreport.activity, $('#report'));
    makeCollapseableTreeFromUnorderedList('report');
}

// Recursively renders the activity and it's children as nested unordered lists
function render(activity, parent) {

    $('<span class="activityTitle" >' + activity.title + '</span>').appendTo(parent);
    var ul = $('<ul>')
        .append(fmtListItem('Satisfied', activity.satisfied))
        .append(fmtListItem('Completed', activity.completed))
        .append(fmtListItem('Progress Status', activity.progressstatus))
        .append(fmtListItem('Atttempts', activity.attempts))
        .append(fmtListItem('Suspended', activity.suspended))
        .append(fmtListObjectiveItems(activity.objectives))
        .append(fmtRuntime(activity.runtime))
        .appendTo(parent);
                    
    // If child activities are defined, render them as well
    if (activity.children !== undefined && activity.children !== null && activity.children !== "") {
        $(activity.children.activity).each(function() {
            render(this, $('<li>').appendTo(ul).get());
        });
    }
}

// Helper to print name/value pairs of activity data
function fmtListItem(name, value) {

    if (value === undefined || value === null) {
        value = "";
    }

    return "<li>" + name + ": <span class='dataValue'>" + value + "</span></li>";
}

// Returns the html of one or more lists items representing objective data
function fmtListObjectiveItems(objectives) {
    
    if (objectives === undefined) {
        return "";
    }   

    var result = "";

    $(objectives.objective).each(function(index) {
      
        result = result + '<li>' +
            $('<li>')
            .append('Activity Objective #' + (index+1))
            .append($('<ul>')
                .append(fmtListItem('Id', this.id))
                .append(fmtListItem('Measure Status', this.measurestatus))
                .append(fmtListItem('Normalized Measure', this.normalizedmeasure))
                .append(fmtListItem('Progress Measure', this.progressstatus))
                .append(fmtListItem('Satisfied Status', this.satisfiedstatus))
            )
            .html() +  '</li>';
    });
  
    return result;
}


// Returns the html of the runtime data if it exists
function fmtRuntime(runtime) {
    
    if (runtime === undefined) {
        return "";

    } else {

        return $('<li>')
            .append('Runtime Data')
            .append($('<ul>')
                .append(fmtListItem('cmi.completion_status', runtime.completion_status))
                .append(fmtListItem('cmi.credit', runtime.credit))
                .append(fmtListItem('cmi.entry', runtime.entry))
                .append(fmtListItem('cmi.exit', runtime.exit))
                .append(fmtLearnerPreference(runtime.learnerpreference))
                .append(fmtListItem('cmi.location', runtime.location))
                .append(fmtListItem('cmi.mode', runtime.mode))
                .append(fmtListItem('cmi.progress_measure', runtime.progress_measure))
                .append(fmtListItem('cmi.score_scaled', runtime.score_scaled))
                .append(fmtListItem('cmi.score_raw', runtime.score_raw))
                .append(fmtListItem('cmi.score_min', runtime.score_min))
                .append(fmtListItem('cmi.score_max', runtime.score_max))
                .append(fmtListItem('cmi.total_time', runtime.total_time))
                .append(fmtListItem('Total Time Tracked by SCORM Engine', runtime.timetracked))
                .append(fmtListItem('cmi.success_status', runtime.success_status))
                .append(fmtListItem('cmi.suspend_data', runtime.suspend_data))
                .append(fmtInteractions(runtime.interactions))
                .append(fmtRtObjectives(runtime.objectives))
                .append(fmtComments(runtime.comments_from_learner, false))
                .append(fmtComments(runtime.comments_from_lms, true))
                .append(fmtStaticData(runtime.static))
             )
    }
}

function fmtLearnerPreference(learner_preference) {
    
    return $('<li>')
        .append('cmi.learner_preference')
        .append($('<ul>')
            .append(fmtListItem('cmi.learner_preference.audio_level', learner_preference.audio_level))
            .append(fmtListItem('cmi.learner_preference.language', learner_preference.language))
            .append(fmtListItem('cmi.learner_preference.delivery_speed', learner_preference.delivery_speed))
            .append(fmtListItem('cmi.learner_preference.audio_captioning', learner_preference.audio_captioning))
         )
}

function fmtStaticData(static) {
    
    return $('<li>')
        .append('Static Data')
        .append($('<ul>')
            .append(fmtListItem('cmi.completion_threshold', static.completion_threshold))
            .append(fmtListItem('cmi.launch_data', static.launch_data))
            .append(fmtListItem('cmi.learner_id', static.learner_id))
            .append(fmtListItem('cmi.learner_name', static.learner_name))
            .append(fmtListItem('cmi.max_time_allowed', static.max_time_allowed))
            .append(fmtListItem('cmi.scaled_passing_score', static.scaled_passing_score))
            .append(fmtListItem('cmi.time_limit_action', static.time_limit_action))
         )
}

// Returns the html of one or more lists items representing objective data
function fmtInteractions(interactions) {
    
    if (interactions === undefined || interactions == null || interactions == "") {
        return "";
    }   

    var result = "";
      
    $(interactions.interaction).each(function(index) {
      
        result = result + '<li>' +
            $('<li>')
            .append('cmi.interactions.' + index)
            .append($('<ul>')
                .append(fmtListItem('cmi.interactions.' + index + '.id', this.id))
                .append(fmtListItem('cmi.interactions.' + index + '.type', this.type))
                .append(fmtListItem('cmi.interactions.' + index + '.timestamp', this.timestamp))
                .append(fmtCorrectResponses('cmi.interactions.' + index + '.correct_responses.', this.correct_responses))
                .append(fmtListItem('cmi.interactions.' + index + '.weighting', this.weighting))
                .append(fmtListItem('cmi.interactions.' + index + '.learner_response', this.learner_response))
                .append(fmtListItem('cmi.interactions.' + index + '.result', this.result))
                .append(fmtListItem('cmi.interactions.' + index + '.latency', this.latency))
                .append(fmtListItem('cmi.interactions.' + index + '.description', this.description))
            )
            .html() + '</li>';
            
    });

    return result;
}

// Returns the html of one or more lists items representing objective data
function fmtComments(comments, fromLms) {
    
    if (comments === undefined || comments == null || comments == "") {
        return "";
    }   
    
    if (fromLms) {
        var commentType = "comments_from_lms";
    } else { 
        var commentType = "comments_from_learner";
    }

    var result = "";
      
    $(comments.comment).each(function(index) {
      
        result = result + '<li>' +
            $('<li>')
            .append('cmi.' + commentType + '.' + index)
            .append($('<ul>')
                .append(fmtListItem('cmi.' + commentType + '.' + index + '.comment', this.value))
                .append(fmtListItem('cmi.' + commentType + '.' + index + '.location', this.location))
                .append(fmtListItem('cmi.' + commentType + '.' + index + '.timestamp', this.timestamp))
            )
            .html() + '</li>';
            
    });

    return result;
}

function fmtCorrectResponses(title, correctResponses) {
    
    if (correctResponses === undefined || correctResponses == null || correctResponses == "") {
        return "";
    }   

    var result = "";
      
    $(correctResponses.response).each(function(index) {
      
        result = result + 
            $('<li>')
            .append(fmtListItem(title + index + '.pattern', this.id))
            .html();
            
    });

    return result;
}

// Returns the html of one or more lists items representing objective data
function fmtRtObjectives(objectives) {
    
    if (objectives === undefined || objectives == null || objectives == '') {
        return "";
    }   

    var result = "";
      
    $(objectives.objective).each(function(index) {
      
        result = result + '<li>' +
            $('<li>')
            .append('cmi.objectives.' + index)
            .append($('<ul>')
                .append(fmtListItem('cmi.objectives.' + index + '.id', this.id))
                .append(fmtListItem('cmi.objectives.' + index + '.score.scaled', this.score_scaled))
                .append(fmtListItem('cmi.objectives.' + index + '.score.raw', this.score_raw))
                .append(fmtListItem('cmi.objectives.' + index + '.score.min', this.score_min))
                .append(fmtListItem('cmi.objectives.' + index + '.score.max', this.score_max))
                .append(fmtListItem('cmi.objectives.' + index + '.success_status', this.success_status))
                .append(fmtListItem('cmi.objectives.' + index + '.completion_status', this.completion_status))
                .append(fmtListItem('cmi.objectives.' + index + '.progress_measure', this.progress_measure))
                .append(fmtListItem('cmi.objectives.' + index + '.description', this.description))
            )
            .html() + '</li>';
            
    });

    return result;
}


// Applies tree control type dhtml collapse/expand functionality to 
// the unordered lists within the specified div.
function makeCollapseableTreeFromUnorderedList(divName) {

  $('#' + divName + ' li:has(ul)')  
    .click(function(event){   
      if (this == event.target) { 
        if ($(this).children().is(':hidden')) {   
          $(this) 
            .css('list-style-image','url(images/minus.gif)') 
            .children().show(); 
        } else { 
          $(this) 
            .css('list-style-image','url(images/plus.gif)') 
            .children().not('span').hide(); 
        } 
      } 
      return false;   
    })
    .css('cursor','pointer')   
    .click();
    
  $('li:not(:has(ul))').css({   
    cursor: 'default', 
    'list-style-image':'none' 
  });
}


    
</script>


<style type="text/css">

    /* CSS Values for the Report */
    .activityTitle { color: blue; font-size: 110% }
    .dataValue {font-weight: bold }
    #report li {list-style: none; padding: 1px }
    #report ul { margin-top: 0; margin-bottom: 0px; font-size: 10pt; }

</style>

</head>
<body>
    <div id="report"/>
</body>
</html>
