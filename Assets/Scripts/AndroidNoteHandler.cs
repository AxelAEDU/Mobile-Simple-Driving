using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

public class AndroidNoteHandler : MonoBehaviour
{
#if UNITY_ANDROID
    private const string ChannelId = "note_channel";

    public void ScheduleNote(DateTime dateTime)
    {
        AndroidNotificationChannel noteChannel = new AndroidNotificationChannel
        {
            Id = ChannelId,
            Name = "Notification Channel",
            Description = "Some random stuff",
            Importance = Importance.Default
        };

        AndroidNotificationCenter.RegisterNotificationChannel(noteChannel);

        AndroidNotification notification = new AndroidNotification
        {
            Title = "Energy Recharged!",
            Text = "Your energy has recharged!",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = dateTime
        };

        AndroidNotificationCenter.SendNotification(notification, ChannelId);
    }
#endif
}
